export interface IProduct{
  id: number;
  name: string;
  description: string;
  pictureUrl: string;
  price: number;
  productType: IProductType;
  productBrand: IProductBrand;
}

export interface IProductBrand{
  id: number;
  name: string;
}

export interface IProductType{
  id: number;
  name: string;
}
