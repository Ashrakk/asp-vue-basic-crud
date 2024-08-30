import { defineStore } from 'pinia';
import {api} from "boot/axios"
import {DifficultyUtils} from "boot/utils/DifficultyUtils";
import {IDifficulty} from "boot/interfaces/IDifficulty";

enum Api_Action {
  Get_All = "difficulty",
  Get_Details = "difficulty/details",
}

export const useDifficultyApiStore = defineStore('difficulty-api', {
  actions: {
    async getAll(): IDifficulty[] | null {
      try {
        const result = await api.get(Api_Action.Get_All);
        return DifficultyUtils.ParseToArray(result.data);
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
        return DifficultyUtils.ParseToModel(result.data);
      }
      catch ( error ) {
        return null;
      }
    },
  }
});
