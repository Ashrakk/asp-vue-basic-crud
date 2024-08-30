import { defineStore } from 'pinia';
import {api} from "boot/axios"
import {RegionUtils} from "boot/utils/RegionUtils";
import {IRegion} from "boot/interfaces/IRegion";

enum Api_Action {
  Get_All = "regions",
  Get_Details = "regions/details",
}

export const useRegionApiStore = defineStore('region-api', {
  actions: {
    async getAll(): IRegion[] | null {
      try {
        const result = await api.get(Api_Action.Get_All);
        return RegionUtils.ParseToArray(result.data);
      }
      catch ( error ) {
        return null;
      }
    },

    async getDetails(id: number) {
      const params = new URLSearchParams({
        'id': id.toString()
      })

      try {
        const result = await api.get(Api_Action.Get_Details, { params } );
        return RegionUtils.ParseToModel(result.data);
      }
      catch ( error ) {
        return null;
      }
    },
  }
});
