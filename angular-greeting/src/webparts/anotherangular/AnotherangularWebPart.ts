import { Version } from '@microsoft/sp-core-library';
import {
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-property-pane';
import { BaseClientSideWebPart } from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './AnotherangularWebPart.module.scss';
import * as strings from 'AnotherangularWebPartStrings';

import * as angular from 'angular';
import './app/app-module';

export interface IAnotherangularWebPartProps {
  description: string;
}

export default class AnotherangularWebPart extends BaseClientSideWebPart <IAnotherangularWebPartProps> {

  public render(): void {
    if (this.renderedOnce === false) {
      this.domElement.innerHTML = `
  <div class="${styles.toDo}">
    <div data-ng-controller="HomeController as vm">
      <div class="${styles.loading}" ng-show="vm.isLoading">
        <div class="${styles.spinner}">
          <div class="${styles.spinnerCircle} ${styles.spinnerLarge}"></div>
          <div class="${styles.spinnerLabel}">Loading...</div>
        </div>
      </div>
      <div ng-show="vm.isLoading === false">
        <div class="${styles.textField} ${styles.underlined}" ng-class="{'${styles.isActive}': vm.newToDoActive}">
          <label for="newToDo" class="${styles.label}">New to do:</label>
          <input type="text" label="New to do:" id="newToDo" value="" class="${styles.field}" aria-invalid="false" ng-model="vm.newItem" ng-keydown="vm.todoKeyDown($event)" ng-focus="vm.newToDoActive = true" ng-blur="vm.newToDoActive = false">
        </div>
      </div>
      <div class="list" ng-show="vm.isLoading === false">
        <div class="listSurface">
          <div class="listPage">
            <div class="listCell" ng-repeat="todo in vm.todoCollection" uif-item="todo" ng-class="{'${styles.done}': todo.done}">
              <div class="${styles.listItem}">
                <span class="${styles.listItemPrimaryText}">{{todo.title}}</span>
                <div class="${styles.listItemActions}">
                  <div class="${styles.listItemAction}" ng-click="vm.completeTodo(todo)" ng-show="todo.done === false">
                    <i class="${styles.icon} ${styles.iconCheckMark}"></i>
                  </div>
                  <div class="${styles.listItemAction}" ng-click="vm.undoTodo(todo)" ng-show="todo.done">
                    <i class="${styles.icon} ${styles.iconUndo}"></i>
                  </div>
                  <div class="${styles.listItemAction}" ng-click="vm.deleteTodo(todo)">
                    <i class="${styles.icon} ${styles.iconRecycleBin}"></i>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>`;

      angular.bootstrap(this.domElement, ['todoapp']);
    }
  }

  protected get dataVersion(): Version {
  return Version.parse('1.0');
}

  protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration {
  return {
    pages: [
      {
        header: {
          description: strings.PropertyPaneDescription
        },
        groups: [
          {
            groupName: strings.BasicGroupName,
            groupFields: [
              PropertyPaneTextField('description', {
                label: strings.DescriptionFieldLabel
              })
            ]
          }
        ]
      }
    ]
  };
}
}
