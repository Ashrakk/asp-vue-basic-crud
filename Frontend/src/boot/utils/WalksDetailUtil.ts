import {IPaginatedResponse} from "boot/interfaces/IPaginatedResponse";
import {IWalksDetail} from "boot/interfaces/IWalksDetail";
import {IWalks} from "boot/interfaces/IWalks";
import {RegionUtils} from "boot/utils/RegionUtils";
import {DifficultyUtils} from "boot/utils/DifficultyUtils";

export abstract class WalksDetailUtil {

  public static ParseToPaginated(response: any): IPaginatedResponse | null {
    const walksData: IWalksDetail[] = response.data.map((item: any): IWalksDetail => ({
      id: item.id,
      description: item.description,
      lengthKm: item.lengthKm,
      image: item.image,
      region: RegionUtils.ParseToModel(item.regions),
      difficulty: DifficultyUtils.ParseToModel(item.difficulty)
    }));

    return {
      data: walksData,
      totalCount: response.totalCount
    };
  }

  public static ParseToModel(item: any): IWalksDetail {
    return {
      id: item.id,
      description: item.description,
      lengthKm: item.lengthKm,
      image: item.image,
      region: RegionUtils.ParseToModel(item.regions),
      difficulty: DifficultyUtils.ParseToModel(item.difficulty)
    };
  }

  public static ConvertToIWalks(item: IWalksDetail): IWalks {

    if (!item.region || typeof item.region.id !== 'number')
      throw new Error("Invalid region");

    if (!item.difficulty || typeof item.difficulty.id !== 'number')
      throw new Error("Invalid difficulty");

    return {
      id: item.id,
      description: item.description,
      lengthKm: item.lengthKm,
      image: item.image,
      regionFK: item.region.id,
      difficultyFK: item.difficulty.id
    };
  }
}
