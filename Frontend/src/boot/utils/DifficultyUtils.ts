import {IDifficulty} from "boot/interfaces/IDifficulty";

export abstract class DifficultyUtils {

  public static ParseToArray(response: any): IDifficulty[] | null {
    return response.map((item: any): IDifficulty => ({
      id: item.id,
      name: item.name,
    }));
  }

  public static ParseToModel(item: any): IDifficulty {
    return {
      id: item.id,
      name: item.name
    };
  }
}
