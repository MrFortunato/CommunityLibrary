import { Component, ViewEncapsulation } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [RouterModule, CommonModule],
  encapsulation: ViewEncapsulation.ShadowDom// Ignora os estilos globais

})

export class HomeComponent {

  books = [
    { title: 'Livro A', author: 'Autor 1', image: 'app/assets/image/image.png' },
    { title: 'Livro B', author: 'Autor 2', image: 'assets/image/image.png' },
    // Adicione mais livros conforme necessário
  ];

  currentPage = 1;
  totalPages = 5;

  login() {
    console.log('Login clicked');
  }

  register() {
    console.log('Register clicked');
  }

  addToCart(book: any) {
    console.log(`Adicionado: ${book.title}`);
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }
}
