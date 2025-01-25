import {Item} from './item';

export interface Magazine extends Item{
  Publisher: string;
  ReleaseDate: Date;
}
