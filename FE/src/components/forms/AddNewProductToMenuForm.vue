<template>
  <div>
    <div class="pop-up-container"></div>
    <div class="pop-up-form">
      <div class="form-container">
        <div class="mt-5">
          <span class="font-weight-bold">Original Price: </span
          >{{ product.price }}
        </div>
        <div>
          <span class="font-weight-bold">Quantity available: </span
          >{{ product.quantity }}
        </div>
        <hr />
        <div class="form-group">
          <label for="categoryName">Quantity</label>
          <input
            type="number"
            class="form-control"
            id="categoryName"
            v-model="newProduct.quantity"
          />
        </div>
        <div class="form-group">
          <label for="categoryName">Price</label>
          <input
            type="number"
            class="form-control"
            id="categoryName"
            v-model="newProduct.price"
          />
        </div>
        <div class="text-center text-danger">{{ errorQuantity }}</div>
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
        {{ newProduct }}
      </div>
    </div>
  </div>
</template>

<script>
import { addProductInMenu } from "../../services/MenuService";

export default {
  name: "category-form",
  async created() {},
  props: {
    product: {
      type: Object,
      default: null,
    },
    menuId: {
      type: Number,
      default: null,
    },
  },
  data() {
    return {
      newProduct: {
        productID: null,
        price: null,
        quantity: null,
      },
      errorQuantity: null,
    };
  },
  computed: {},
  methods: {
    closeForm() {
      this.$emit("input", false);
    },
    async save() {
      if (this.newProduct.quantity > this.product.quantity) {
        this.errorQuantity = "Quantity is not enough";
      } else {
        this.newProduct.productID = this.product.id;
        const spData = {
          menuId: this.menuId,
          data: this.newProduct,
        };

        const result = await addProductInMenu(spData);

        if (result) {
          const paramsGetProductMin = {
            id: this.menuId,
            queryString: {
              all: true,
            },
          };

          await this.$store.dispatch(
            "menu/getProductInMenuMin",
            paramsGetProductMin
          );

          this.$emit("input", false);
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
  height: 600px;
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