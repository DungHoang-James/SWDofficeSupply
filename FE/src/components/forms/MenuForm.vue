<template>
  <div>
    <div class="pop-up-container"></div>
    <div class="pop-up-form">
      <p>Menu</p>
      <div class="form-container">
        <div class="form-group">
          <label for="txtName">Name</label>
          <input
            type="text"
            class="form-control"
            id="txtName"
            v-model="menu.name"
          />
        </div>
        <div class="text-danger text-center">{{ errorMessage }}</div>
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
import { createMenu, updateMenu } from "../../services/MenuService";

export default {
  name: "menu-form",

  async created() {
    if (this.id) {
      await this.$store.dispatch("menu/getMenu", this.id);

      this.menu = { ...this.menuData };
    }
  },

  data() {
    return {
      menu: {
        id: null,
        name: null,
        adminID: null,
        isDelete: null,
      },
      errorMessage: null,
    };
  },

  computed: {
    menuData() {
      return this.$store.state.menu.menu;
    },
  },

  props: {
    id: {
      type: Number,
      default: null,
    },
  },

  methods: {
    closeForm() {
      this.$emit("input", false);
    },

    async save() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

      if (this.menu.id) {
        const result = await updateMenu(this.menu);
console.log(result);
        if (result) {
          const params = { adminID: userInfo.id };
          await this.$store.dispatch("menu/getMenus", params);
        }
      } else {
        this.menu.adminID = userInfo.id;
        const result = await createMenu(this.menu);

        if (result) {
          const params = { adminID: userInfo.id };
          await this.$store.dispatch("menu/getMenus", params);
        }
      }

      this.$emit("input", false);
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
  height: 400px;
  background: #ffffff;
  border-radius: 15px;
  top: 15%;
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
  width: 500px;
}

.button-container {
  width: 26.5rem;
  margin: 0 auto;
  margin-top: 50px;
}
</style>