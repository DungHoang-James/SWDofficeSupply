<template>
  <div>
    <div class="pop-up-container"></div>
    <div class="pop-up-form" v-if="category">
      <p>Category Infomation</p>
      <div class="form-container">
        <div class="form-group">
          <label for="categoryName">Name category</label>
          <input
            type="text"
            class="form-control"
            id="categoryName"
            placeholder="Enter name of category"
            v-model="category.name"
          />
        </div>
        <div class="form-group">
          <label for="categoryDescription">Description</label>
          <textarea
            class="form-control"
            id="categoryDescription"
            rows="3"
            v-model="category.description"
          ></textarea>
        </div>
        <div class="form-row" v-if="allowDelete">
          <label class="mr-3">Deleted:</label>
          <base-switch
            v-model="category.isDelete"
            :value="category.isDelete"
          ></base-switch>
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

    <!-- form 2 -->
    <div class="pop-up-form" v-else>
      <p>Category Infomation</p>
      <div class="form-container">
        <div class="form-group">
          <label for="categoryName">Name category</label>
          <input
            type="text"
            class="form-control"
            id="categoryName"
            placeholder="Enter name of category"
            v-model="newCategory.name"
          />
        </div>
        <div class="form-group">
          <label for="categoryDescription">Description</label>
          <textarea
            class="form-control"
            id="categoryDescription"
            rows="3"
            v-model="newCategory.description"
          ></textarea>
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
export default {
  name: "category-form",
  async created() {
    await this.$store.dispatch("category/getCategory", this.id);
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
      newCategory: {
        name: null,
        description: null,
      },
    };
  },
  computed: {
    category() {
      return this.$store.state.category.category;
    },
  },
  methods: {
    closeForm() {
      this.$emit("input", false);
    },
    async save() {
      if (this.category) {
        await this.$store.dispatch("category/updateCategory", this.category);
        await this.$store.dispatch("category/getCategoryAction");
      } else {
        await this.$store.dispatch("category/createCategory", this.newCategory);
        await this.$store.dispatch("category/getCategoryAction");
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
  height: 500px;
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
}

.button-container {
  width: 26.5rem;
  margin: 0 auto;
  margin-top: 50px;
}
</style>