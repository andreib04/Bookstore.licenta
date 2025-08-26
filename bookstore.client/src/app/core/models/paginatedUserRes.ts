import {User} from './user';

export interface PaginatedUserRes {
  items: User[];
  totalCount: number;
}
