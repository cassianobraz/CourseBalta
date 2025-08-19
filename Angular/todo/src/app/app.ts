import { Todo } from './../models/todo.model';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [ CommonModule, ReactiveFormsModule ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  public todos: Todo[] = [];
  public title: string = 'My tasks'
  public form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      title: [ '', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(60),
        Validators.required
      ]) ]
    });

    this.load();
  }

  add() {
    const title = this.form.controls[ 'title' ].value;
    const id = this.todos.length + 1;
    this.todos.push(new Todo(id, title, false));
    this.save();
    this.clear();
  }

  clear() {
    this.form.reset();
  }

  remove(todo: Todo) {
    const index = this.todos.indexOf(todo);

    if (index !== -1)
      this.todos.splice(index, 1);

    this.save();
  }

  markAsDone(todo: Todo) {
    todo.done = true;
    this.save();
  }

  markAsUnDone(todo: Todo) {
    todo.done = false;
    this.save();
  }

  save() {
    const data = JSON.stringify(this.todos);
    localStorage.setItem('todos', data);
  }

  load() {
    const data = localStorage.getItem('todos');
    this.todos = JSON.parse(data!);
  }
}
