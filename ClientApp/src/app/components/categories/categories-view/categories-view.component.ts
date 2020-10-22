import { Component } from "@angular/core";
import {CategoryService} from "../../../services/category.service";
import {of as ofObservable} from "rxjs/observable/of";

import {Observable} from "rxjs/Observable";
import {FlatTreeControl} from "@angular/cdk/tree";
import {MatTreeFlatDataSource, MatTreeFlattener} from "@angular/material";
import {
  ChecklistDatabase,
  TodoItemFlatNode, TodoItemNode
} from "../symptom-categories-create/symptom-categories-create.component";
import {Router} from "@angular/router";

@Component({
  selector: 'max-categories-view',
  templateUrl: './categories-view.component.html',
  styleUrls: ['./categories-view.component.scss'],
  providers: [ChecklistDatabase]
})
export class CategoriesViewComponent  {

  setId: number;

  /** Map from flat node to nested node. This helps us finding the nested node to be modified */
  flatNodeMap: Map<TodoItemFlatNode, TodoItemNode> = new Map<TodoItemFlatNode, TodoItemNode>();

  /** Map from nested node to flattened node. This helps us to keep the same object for selection */
  nestedNodeMap: Map<TodoItemNode, TodoItemFlatNode> = new Map<TodoItemNode, TodoItemFlatNode>();

  treeControl: FlatTreeControl<TodoItemFlatNode>;

  treeFlattener: MatTreeFlattener<TodoItemNode, TodoItemFlatNode>;

  dataSource: MatTreeFlatDataSource<TodoItemNode, TodoItemFlatNode>;

  constructor(private database: ChecklistDatabase, private categoryService: CategoryService, private router: Router) {
    this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,
      this.isExpandable, this.getChildren);
    this.treeControl = new FlatTreeControl<TodoItemFlatNode>(this.getLevel, this.isExpandable);
    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

    database.dataChange.subscribe(data => {
      this.dataSource.data = data;
    });
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

  routeToCategory(node: any){
    let flatNode = this.flatNodeMap.get(node);
    this.setId = flatNode.id;
    //this.router.navigateByUrl("categories/" + flatNode.id);
  }

}
