<template>
  <div>
    <div class="pop-up-container"></div>
    <div class="pop-up-form">
      <p>Area Infomation</p>
      <div class="form-container">
        <div class="form-group">
          <label for="categoryName">Name Area</label>
          <input
            type="text"
            class="form-control"
            id="categoryName"
            placeholder="Enter name of area"
            v-model="area.name"
          />
        </div>
        <div class="form-group">
          <label for="categoryDescription">Location</label>
          <input
            class="form-control"
            id="categoryDescription"
            v-model="area.location"
          />
        </div>
        <div class="form-group">
          <label>Menu</label>
          <select v-model="area.menuID" class="form-control">
            <option v-for="menu in menusMin" :key="menu.id" :value="menu.id">
              {{ menu.name }}
            </option>
          </select>
        </div>
        <div class="form-row" v-if="allowDelete">
          <label class="mr-3">Deleted:</label>
          <base-switch
            v-model="area.isDelete"
            :value="area.isDelete"
          ></base-switch>
        </div>
        <div class="text-center text-success" v-if="successMessage">
          {{ successMessage }}
        </div>
        <div class="text-center text-danger" v-if="errorMessage">
          {{ errorMessage }}
        </div>
        <div class="row button-container">
          <button type="button" class="btn btn-success col" @click="save">
            Save
          </button>
          <button
            type="button"
            class="btn btn-outline-danger col"
            @click="closeForm"
          >
            Cannel
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { createArea, updateArea } from "../../services/AreaService";

export default {
  name: "area-form",
  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

    const params = {
      adminID: userInfo.id,
      all: true,
    };

    await this.$store.dispatch("menu/getMenusMin", params);
    if (this.id) {
      await this.$store.dispatch("area/getArea", this.id);
      this.area = { ...this.areaData };
    }
  },
  props: {
    id: {
      type: Number,
      default: null,
    },
    allowDelete: {
      type: Boolean,
      default: null,
    },
  },
  data() {
    return {
      area: {
        id: null,
        name: null,
        location: null,
        menuID: null,
        isDelete: null,
      },
      successMessage: null,
      errorMessage: null,
    };
  },
  computed: {
    areaData() {
      return this.$store.state.area.area;
    },
    menusMin() {
      return this.$store.state.menu.menusMin;
    },
  },
  methods: {
    closeForm() {
      this.$emit("input", false);
    },
    async save() {
      if (this.area.id) {
        const result = await updateArea(this.area);

        if (result) {
          await this.$store.dispatch("area/getAreas");

          this.$emit("input", false);
        }
      } else {
        const result = await createArea(this.area);
        if (result.isSuccess) {
          await this.$store.dispatch("area/getAreas");

          this.$emit("input", false);
        } else {
          this.errorMessage = "Create area fail";
        }
      }
    },
  },
};
</script>

<style scoped>
.pop-up-container {
  width: 100vw;
  height: 100vh;
  background: #5551515d;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 3;
}
.pop-up-form {
  position: fixed;
  width: 500px;
  height: 700px;
  background: #ffffff;
  border-radius: 15px;
  top: 3%;
  left: 35%;
  z-index: 4;
}

.pop-up-form p {
  text-align: center;
  margin-top: 30px;
  font-size: 30px;
  font-weight: bold;
  text-transform: uppercase;
}

.form-container {
  padding: 10px 30px 0 30px;
}

.button-container {
  width: 26.5rem;
  margin: 0 auto;
  margin-top: 50px;
}
</style>