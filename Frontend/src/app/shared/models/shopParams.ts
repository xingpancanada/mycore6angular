///////99.Adding the pagination functionality part 2

export class ShopParams{
  brandId: number = 0;
  typeId: number = 0;

  sort = 'name';
  pageNumber = 1;
  pageSize = 6;

  search: string = '';
}
