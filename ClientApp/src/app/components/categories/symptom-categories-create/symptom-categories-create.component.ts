import {Component, EventEmitter, Injectable, Input, OnChanges, Output, ViewEncapsulation} from "@angular/core";
import {SelectionModel} from "@angular/cdk/collections";
import {FlatTreeControl} from "@angular/cdk/tree";
import {MatTreeFlattener, MatTreeFlatDataSource} from "@angular/material";
import {of as ofObservable} from "rxjs/observable/of";
import {Observable} from "rxjs/Observable";
import {BehaviorSubject} from "rxjs/BehaviorSubject";
import {CategoryService} from "../../../services/category.service";
import {MapCategory} from "../../../models/mapCategory.model";
import {SaveCategory} from "../../../models/saveCategory.model";

/**
 * Node for to-do item
 */
export class TodoItemNode {
  children: TodoItemNode[];
  item: string;
  id: number;
}

/** Flat to-do item node with expandable and level information */
export class TodoItemFlatNode {
  item: string;
  level: number;
  expandable: boolean;
}

/**
 * Checklist database, it can build a tree structured Json object.
 * Each node in Json object represents a to-do item or a category.
 * If a node is a category, it has children items and new items can be added under the category.
 */
@Injectable()
export class ChecklistDatabase {
  dataChange: BehaviorSubject<TodoItemNode[]> = new BehaviorSubject<TodoItemNode[]>([]);

  get data(): TodoItemNode[] { return this.dataChange.value; }

  categoryMap = new Map<number,MapCategory>();

  constructor(private categoryService : CategoryService) {
    this.initialize();
  }

  initialize() {
    // Build the tree nodes from Json object. The result is a list of `TodoItemNode` with nested
    //     file node as children.
    let categories: MapCategory[];

    let rootCategories: MapCategory[] = [];
    let rootCategory: MapCategory[] = [];
    let categoryIds: number[];
    let data: any;

    this.categoryService.getCategories().subscribe(categories => {

      categories.forEach(category =>
        this.categoryMap.set(category.categoryId, category));



      rootCategory.push(categories.filter(c => c.name == "root").pop());
      console.log("rootcategory", rootCategory);
      data = this.buildFileTree(rootCategory, 0);
      this.dataChange.next(data);

      });

  }

  /**
   * Build the file structure tree. The `value` is the Json object, or a sub-tree of a Json object.
   * The return value is the list of `TodoItemNode`.
   */

  categoryTransformer(subcategories: any[]): MapCategory[]{

    let result: MapCategory[] =[];
    let ids = subcategories.map(s => s.subcategoryId);
    ids.forEach(id => result.push(this.categoryMap.get(id)));
    return result;

  }


  buildFileTree(value: MapCategory[], level: number) {
    let data: any[] = [];
    value.forEach(category => {
      if(category != null){


      let node = new TodoItemNode();
      node.item = category.name;
      node.id = category.categoryId;

        if(category.subcategories.length > 0){
          let newcategories = this.categoryTransformer(category.subcategories);
          node.children = this.buildFileTree(newcategories, level +1);
        }

      data.push(node);
    }});
    return data;
  }

  /** Add an item to to-do list */
  insertItem(parent: TodoItemNode, name: string) {
    const child = <TodoItemNode>{item: name};
    if(parent.children == null)
      parent.children = [];
    parent.children.push(child);
    this.dataChange.next(this.data);

  }

  removeItem(parent: TodoItemNode, nodeToDelete: TodoItemNode) {

    if (parent.children) {
      parent.children = parent.children.filter(c => c.item !== '');

      this.dataChange.next(this.data);
    }
  }

  updateItem(node: TodoItemNode, name: string, id?: number | null) {
    node.item = name;
    node.id = id;
    this.dataChange.next(this.data);
  }
}


@Component({
  selector: 'max-symptom-categories-create',
  templateUrl: './symptom-categories-create.component.html',
  styleUrls: ['./symptom-categories-create.component.scss'],
  providers: [ChecklistDatabase],
  encapsulation: ViewEncapsulation.None
})
export class SymptomCategoriesCreateComponent implements OnChanges{

  @Input("selectedCategoryId") categoryIdForSymptom: number;
  @Output("selectedCategoryIdChange") categoryIdForSymptomChange = new EventEmitter<number>();

  oldParent: TodoItemNode;

  /** Map from flat node to nested node. This helps us finding the nested node to be modified */
  flatNodeMap: Map<TodoItemFlatNode, TodoItemNode> = new Map<TodoItemFlatNode, TodoItemNode>();

  /** Map from nested node to flattened node. This helps us to keep the same object for selection */
  nestedNodeMap: Map<TodoItemNode, TodoItemFlatNode> = new Map<TodoItemNode, TodoItemFlatNode>();

  /** A selected parent node to be inserted */
  selectedParent: TodoItemFlatNode | null = null;

  parentId: number;

  /** The new item's name */
  newItemName: string = '';

  treeControl: FlatTreeControl<TodoItemFlatNode>;

  treeFlattener: MatTreeFlattener<TodoItemNode, TodoItemFlatNode>;

  dataSource: MatTreeFlatDataSource<TodoItemNode, TodoItemFlatNode>;

  /** The selection for checklist */
  checklistSelection = new SelectionModel<TodoItemFlatNode>(false /* multiple */);

  constructor(private database: ChecklistDatabase, private categoryService: CategoryService) {
    this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,
      this.isExpandable, this.getChildren);
    this.treeControl = new FlatTreeControl<TodoItemFlatNode>(this.getLevel, this.isExpandable);
    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

    database.dataChange.subscribe(data => {
      this.dataSource.data = data;
    });
  }

  ngOnChanges(){
    let id = this.categoryIdForSymptom;
    if(id){
      let checkedCategoryFlatNode:TodoItemFlatNode;
      this.flatNodeMap.forEach((flatNode, node) => {
        if(flatNode.id == id){
          checkedCategoryFlatNode = node;
        }
      });
      if(checkedCategoryFlatNode){
          this.checklistSelection = new SelectionModel<TodoItemFlatNode>(false, [checkedCategoryFlatNode]);
      }
    }
  }

  getLevel = (node: TodoItemFlatNode) => { return node.level; };

  isExpandable = (node: TodoItemFlatNode) => { return node.expandable; };

  getChildren = (node: TodoItemNode): Observable<TodoItemNode[]> => {
    return ofObservable(node.children);
  }

  hasChild = (_: number, _nodeData: TodoItemFlatNode) => { return _nodeData.expandable; };

  hasNoContent = (_: number, _nodeData: TodoItemFlatNode) => { return _nodeData.item === ''; };

  /**
   * Transformer to convert nested node to flat node. Record the nodes in maps for later use.
   */
  transformer = (node: TodoItemNode, level: number) => {
    let flatNode = this.nestedNodeMap.has(node) && this.nestedNodeMap.get(node)!.item === node.item
      ? this.nestedNodeMap.get(node)!
      : new TodoItemFlatNode();
    flatNode.item = node.item;
    flatNode.level = level;
    flatNode.expandable = !!node.children;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  }

  /** Whether all the descendants of the node are selected */
  descendantsAllSelected(node: TodoItemFlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    return descendants.every(child => this.checklistSelection.isSelected(child));
  }

  /** Whether part of the descendants are selected */
  descendantsPartiallySelected(node: TodoItemFlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const result = descendants.some(child => this.checklistSelection.isSelected(child));
    return result && !this.descendantsAllSelected(node);
  }

  /** Toggle the to-do item selection. Select/deselect all the descendants node */
  todoItemSelectionToggle(node: TodoItemFlatNode): void {
    this.checklistSelection.toggle(node);
    let id = this.flatNodeMap.get(node).id;
    this.categoryIdForSymptomChange.emit(id);
  }

  /** Select the category so we can insert the new item. */
  addNewItem(node: TodoItemFlatNode) {

    if (this.oldParent != null) {
      let nodeToDelete = this.flatNodeMap.get(node);
      this.database.removeItem(this.oldParent, nodeToDelete);
      this.oldParent = null;
    }
    let parentNode = this.flatNodeMap.get(node);
    this.parentId = parentNode.id;
    this.database.insertItem(parentNode!, '');
    this.oldParent = parentNode;
    this.treeControl.expand(node);
  }

  /** Save the node to database */
  saveNode(node: TodoItemFlatNode, itemValue: string) {

    if(itemValue.length >0){
      const nestedNode = this.flatNodeMap.get(node);

      let saveCategory: SaveCategory ={name: itemValue, parentId: this.parentId};


      this.categoryService.createCategory(saveCategory).subscribe( c => {

              let id = c.subcategories
                .filter(c => c.name == itemValue)
                .map(c => c.subcategoryId)
                .pop();

              this.database.updateItem(nestedNode!, itemValue,id)

        });
    }
  }
}
