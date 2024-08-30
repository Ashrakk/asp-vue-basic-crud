import {IPaginatedResponse} from "boot/interfaces/IPaginatedResponse";
import {IWalks} from "boot/interfaces/IWalks";
import {IWalksDetail} from "boot/interfaces/IWalksDetail";

export abstract class WalksUtil {

  public static ParseToModel(item: any): IWalks {
    return {
      id: item.id,
      description: item.description,
      lengthKm: item.lengthKm,
      image: item.image,
      regionFK: item.regionFK,
      difficultyFK: item.difficultyFK
    };
  }
}
