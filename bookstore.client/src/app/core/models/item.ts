import {Category} from './category';

export interface Item{
  Id: number;
  Title: string;
  Description: string;
  Price: number;
  Stock: number;
  ItemType: string;
  Image: string;
  CategoryId: number;
  Category: Category;
}
