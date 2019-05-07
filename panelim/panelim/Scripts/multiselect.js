import { inject, bindable, bindingMode, TaskQueue } from 'aurelia-framework';

@inject(TaskQueue)
export class MultiselectCustomElement {

    @bindable options;
    @bindable({ defaultBindingMode: bindingMode.twoWay }) value = [];

    constructor(taskQueue) {
        this.taskQueue = taskQueue;
    }

    attached() {
        $(this.select).multiselect({
            onChange: (option, checked) => {
                if (checked) {
                    this.value.push(option[0].value);
                } else {
                    let index = this.value.indexOf(option[0].value);
                    this.value.splice(index, 1);
                }
            }
        });
    }

    optionsChanged(newValue, oldValue) {
        if (oldValue) {
            this.taskQueue.queueTask(() => {
                this.value = [];
                $(this.select).multiselect('rebuild');
            });
        }
    }
}