import {IDifficulty} from "boot/interfaces/IDifficulty";
import {IRegion} from "boot/interfaces/IRegion";

export interface IWalksDetail {
  id?: Number,
  description: string,
  lengthKm: number,
  image: string,
  region: IRegion,
  difficulty: IDifficulty
}
