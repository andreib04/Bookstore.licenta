import {Book} from './book';

export interface PaginatedBookRes{
  books: Book[];
  totalCount: number;
}
