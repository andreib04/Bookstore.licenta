import {Book} from './book';

export interface PaginatedBookRes{
  items: Book[];
  totalCount: number;
}
