import {IRegion} from "boot/interfaces/IRegion";

export abstract class RegionUtils {

  public static ParseToArray(response: any): IRegion[] | null {
    return response.map((item: any): IRegion => ({
      id: item.id,
      name: item.name,
      image: item.image,
      code: item.code
    }));
  }

  public static ParseToModel(item: any): IRegion {
    return {
      id: item.id,
      name: item.name,
      image: item.image,
      code: item.code
    };
  }
}
