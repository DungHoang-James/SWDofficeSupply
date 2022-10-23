<template>
  <div class="card shadow">
    <div
      class="card-header border-0"
      :class="type === 'dark' ? 'bg-transparent' : ''"
    >
      <div class="row align-items-center">
        <div class="col">
          <h3 class="mb-0" :class="type === 'dark' ? 'text-white' : ''">
            {{ title }}
          </h3>
        </div>
        <div class="col text-right">
          <base-button
            outline
            type="success"
            icon="fas fa-plus"
            @click="closeShoppingTable"
            >Back</base-button
          >
        </div>
      </div>
    </div>
    <div>
      <div class="grid">
        <article v-for="p in products" :key="p.id">
          <img :src="p.imageUrl" alt="" style="width: 250px; height: 250px" />
          <div class="text">
            <h3>{{ p.name }}</h3>
            <p>Price: {{ p.price }} VND</p>
            <p>Quantity: {{ p.quantity }}</p>
            <button @click="addToMenu(p)">Add To Menu</button>
          </div>
        </article>
      </div>
    </div>
  </div>

  <div
    class="card-footer d-flex justify-content-end"
    :class="type === 'dark' ? 'bg-transparent' : ''"
  >
    <base-pagination
      :pageCount="pagination.totalPage"
      :value="pagination.currentPage"
      @input="changePage"
    ></base-pagination>
  </div>

  <!-- toast -->
  <div
    class="position-fixed bottom-0 right-0 p-3 mb-4 mr-3"
    style="
      z-index: 5;
      background: #b3b3cc;
      border-radius: 10px;
    "
    v-if="showToast"
  >
    <div
      id="liveToast"
      class="toast hide"
      role="alert"
      aria-live="assertive"
      aria-atomic="true"
      data-delay="2000"
    >
      <div class="toast-header">
        <strong class="mr-auto">Notification</strong>
        <button
          type="button"
          class="ml-2 mb-1 close"
          data-dismiss="toast"
          aria-label="Close"
          @click="showToast = false"
        >
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="toast-body">{{ message }}</div>
    </div>
  </div>

  <AddNewProductToMenuForm
    @input="closeForm"
    v-if="isShowForm"
    :product="product"
    :menuId="id"
  />
</template>

<script>
import AddNewProductToMenuForm from "../../components/forms/AddNewProductToMenuForm.vue";

export default {
  name: "category-table",

  components: { AddNewProductToMenuForm },

  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
    const params = {
      userID: userInfo.id,
    };
    await this.$store.dispatch("product/getProducts", params);

    const paramsGetProductMin = {
      id: this.id,
      queryString: {
        all: true,
      },
    };

    await this.$store.dispatch("menu/getProductInMenuMin", paramsGetProductMin);
  },

  data() {
    return {
      pagination: {
        totalPage: 1,
        currentPage: 1,
      },
      message: null,
      showToast: false,
      product: null,
      isShowForm: false,
    };
  },
  props: {
    type: {
      type: String,
    },
    title: String,
    id: {
      type: Number,
    },
  },
  computed: {
    products() {
      return this.$store.state.product.products.items;
    },
    productInMenuMin() {
      return this.$store.state.menu.productInMenuMin;
    },
  },
  methods: {
    changePage(value) {
      this.pagination.currentPage = value;
    },
    closeShoppingTable() {
      this.$emit("input", false);
    },
    closeForm(value) {
      this.isShowForm = value;
    },
    addToMenu(p) {
      this.product = null;

      const productId = this.productInMenuMin.find((o) => o.productID === p.id);

      if (productId) {
        this.message = "This product is already in the menu";
        this.showToast = true;
      } else {
        this.product = p;
        this.isShowForm = true;
        // this.message = "Successfully added this product";
        // this.showToast = true;
      }
    },
  },
};
</script>

<style scoped>
.grid {
  margin: 80px;
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  grid-gap: 30px;
  align-items: center;
}

.grid > article {
  background: #eee5e9;
  border: none;
  box-shadow: 2px 2px 6px 0px rgba(0, 0, 0, 0.3);
  border-radius: 20px;
  text-align: center;
  width: 250px;
  height: 440px;
  transition: transform 0.3s;
}

.grid > article:hover {
  transform: translateY(5px);
  box-shadow: 2px 2px 26px 0px rgba(0, 0, 0, 0.3);
}

.grid > article img {
  width: 100%;
  border-top-left-radius: 20px;
  border-top-right-radius: 20px;
}

.text {
  padding: 0 20px 20px;
}

.text h3 {
  text-transform: uppercase;
}

.text button {
  background: #ef6f6c;
  border-radius: 20px;
  border: none;
  color: #fff;
  padding: 10px;
  width: 100%;
  font-weight: 600;
  text-transform: uppercase;
  cursor: pointer;
}

/*  */
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
  width: 1000px;
  height: 700px;
  background: #ffffff;
  border-radius: 15px;
  top: 5%;
  left: 20%;
  z-index: 4;
}

.card-form p {
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

.previewImage {
  width: 440px;
  height: 325px;
  border-radius: 25px;
  border: 1px solid;
}
</style>