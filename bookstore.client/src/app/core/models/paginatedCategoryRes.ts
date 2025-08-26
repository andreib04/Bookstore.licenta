import {Category} from './category';

export interface PaginatedCategoryRes{
  items: Category[];
  totalCount: number;
}
