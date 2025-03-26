import {Item} from './item';

export interface Magazine extends Item{
  publisher: string;
  releaseDate: Date;
}
