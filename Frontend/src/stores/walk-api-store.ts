import { defineStore } from 'pinia';
import {api} from "boot/axios"
import {IWalks} from "boot/interfaces/IWalks";
import {IPaginatedResponse} from "boot/interfaces/IPaginatedResponse";
import {WalksDetailUtil} from "boot/utils/WalksDetailUtil";

enum Api_Action {
  Get_Page = "walks",
  Get_Details = "walks/details",
  Post_Create = "walks/create",
  Post_Update = "walks/update",
  Post_Delete = "walks/delete"
}

export const useWalkApiStore = defineStore('walk-api', {
  actions: {
    async getPage(page: number, limit: number, diffId?: number, regionId?: number): IPaginatedResponse | null {

      const params = new URLSearchParams({
        'page': page.toString(),
        'limit': limit.toString(),
      });

      /*
      let filters = new Object([
        {'DifficultyFK': diffId },
      ]);
      params.append('filters', JSON.stringify(filters));
      */

      if(diffId)
        params.append('difficultyFK', diffId.toString());
      if(regionId)
        params.append('regionFK', regionId.toString());

      try {
        const result = await api.get(Api_Action.Get_Page, { params } );
        return WalksDetailUtil.ParseToPaginated(result.data);
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
        return WalksDetailUtil.ParseToModel(result.data);
      }
      catch ( error ) {
        return null;
      }
    },

    async postCreate(model: IWalks): boolean {
      let result;

      try {
        result = await api.post(Api_Action.Post_Create, JSON.stringify(model), {
          headers: {
            'Content-Type': 'application/json'
          } });
      }
      catch ( error ) {
        return false;
      }

      if(result.status == 201)
        return true;
      else
        return false;
    },

    async postUpdate(model: IWalks) {
      let result;

      try {
        result = await api.post(Api_Action.Post_Update, JSON.stringify(model), {
          headers: {
            'Content-Type': 'application/json'
          } });
      }
      catch ( error ) {
        return false;
      }

      if(result.status == 200)
        return true;
      else
        return false;
    },

    async postDelete(id: number) {
      let result;

      try {
        result = await api.post(Api_Action.Post_Delete, id, {
          headers: {
            'Content-Type': 'application/json'
          } });
      }
      catch ( error ) {
        return false;
      }

      if(result.status == 200)
        return true;
      else
        return false;
    }
  }
});
