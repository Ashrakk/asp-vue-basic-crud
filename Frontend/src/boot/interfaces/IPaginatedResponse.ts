import {IWalksDetail} from "boot/interfaces/IWalksDetail";

export interface IPaginatedResponse {
  data: IWalksDetail[],
  totalCount: number;
}
