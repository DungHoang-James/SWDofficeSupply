<template>
  <div class="card shadow mb-3 p-4" v-if="!isOpenForm">
    <div class="row">
      <div class="form-group col-5">
        <label>Name</label>
        <input
          type="text"
          class="form-control"
          v-model="searchValue.txtSearchName"
        />
      </div>

      <div class="form-group col-2 d-flex flex-column justify-content-end">
        <button class="btn btn-primary" @click="search">Search</button>
      </div>
    </div>
  </div>

  <div class="card shadow" v-if="!isOpenForm">
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
            @click="openForm(null)"
            >Create</base-button
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
            <button @click="openForm(p.id)">Detail</button>
          </div>
        </article>
      </div>
    </div>
  </div>

  <div
    class="card-footer d-flex justify-content-end"
    :class="type === 'dark' ? 'bg-transparent' : ''"
    v-if="!isOpenForm"
  >
    <base-pagination
      :pageCount="pagination.totalPage"
      :value="pagination.currentPage"
      @input="changePage"
    ></base-pagination>
  </div>

  <!-- <ProductForm v-if="isOpenForm" @input="closeForm" /> -->

  <div class="card card-form shadow mb-4 pb-4" v-if="isOpenForm">
    <p>Product</p>
    <div class="d-flex flex-row">
      <div class="form-container">
        <div class="form-group">
          <label for="txtName">Name Product</label>
          <input
            type="text"
            class="form-control"
            id="txtName"
            placeholder="Enter name of category"
            v-model="product.name"
          />
        </div>
        <div class="form-group">
          <label for="txtQuantity">Quantity</label>
          <input
            class="form-control"
            id="txtQuantity"
            type="number"
            v-model="product.quantity"
          />
        </div>
        <div class="form-group">
          <label for="txtPrice">Price</label>
          <input
            class="form-control"
            id="txtPrice"
            type="number"
            v-model="product.price"
          />
        </div>
        <div class="form-group">
          <label>Category</label>
          <select class="form-control" v-model="product.categoryID">
            <option
              v-for="cate in categoriesMin"
              :key="cate.id"
              :value="cate.id"
            >
              {{ cate.name }}
            </option>
          </select>
        </div>
        <div class="form-group">
          <label>Supplier</label>
          <select class="form-control" v-model="product.supplierID">
            <option v-for="sup in suppliersMin" :key="sup.id" :value="sup.id">
              {{ sup.name }}
            </option>
          </select>
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

      <div class="form-container">
        <div class="form-group">
          <label>Upload Image</label>
          <div class="custom-file">
            <input
              type="file"
              class="custom-file-input form-control"
              id="customFile"
              @change="fileChange"
            />
            <label class="custom-file-label" for="customFile"
              >Choose file</label
            >
          </div>
        </div>
        <div>
          <img :src="picture" class="previewImage" v-if="picture" />
          <img src="../../assets/no_image.png" class="previewImage" v-else />
        </div>
        <div class="progress mt-4">
          <div
            class="progress-bar progress-bar-striped bg-success"
            role="progressbar"
            v-bind:style="{ width: `${uploadValue}%` }"
            :aria-valuenow="uploadValue"
            aria-valuemin="0"
            aria-valuemax="100"
          >
            {{ uploadValue }}
          </div>
        </div>
        <button
          type="button"
          class="btn btn-outline-primary col"
          @click="onUpload"
        >
          Upload
        </button>
        <div class="text-center text-danger" v-if="errorMessage">
          {{ errorMessage }}
        </div>
        <div class="text-center text-success" v-if="successMessage">
          {{ successMessage }}
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { ref, getDownloadURL, uploadBytesResumable } from "firebase/storage";
import { storage } from "../../firebaseConfig";
import { createProduct, updateProduct } from "../../services/ProductService";

export default {
  name: "category-table",

  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
    const params = {
      userID: userInfo.id,
    };
    await this.$store.dispatch("product/getProducts", params);

    const productState = this.$store.state.product.products;
    this.pagination.currentPage = productState.currentPage;
    this.pagination.totalPage = productState.totalPage;
  },

  data() {
    return {
      pagination: {
        totalPage: 1,
        currentPage: 1,
      },
      isOpenForm: false,
      imageData: null,
      picture: null,
      uploadValue: 0,
      productId: null,
      product: {
        id: null,
        name: null,
        quantity: null,
        price: null,
        imageUrl: null,
        categoryID: null,
        supplierID: null,
      },
      successMessage: null,
      errorMessage: null,
      searchValue: {
        txtSearchName: "",
      },
    };
  },
  props: {
    type: {
      type: String,
    },
    title: String,
  },
  computed: {
    products() {
      return this.$store.state.product.products.items;
    },
    categoriesMin() {
      return this.$store.state.category.categoriesMin;
    },

    suppliersMin() {
      return this.$store.state.supplier.suppliersMin;
    },
  },
  methods: {
    async changePage(value) {
      this.pagination.currentPage = value;

      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      const params = {
        userID: userInfo.id,
        pageNumber: this.pagination.currentPage,
        name:
          this.searchValue.txtSearchName.length === 0
            ? null
            : this.searchValue.txtSearchName,
      };
      await this.$store.dispatch("product/getProducts", params);

      const productState = this.$store.state.product.products;
      this.pagination.currentPage = productState.currentPage;
      this.pagination.totalPage = productState.totalPage;
    },

    async search() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      const params = {
        userID: userInfo.id,
        name:
          this.searchValue.txtSearchName.length === 0
            ? null
            : this.searchValue.txtSearchName,
      };
      await this.$store.dispatch("product/getProducts", params);

      const productState = this.$store.state.product.products;
      this.pagination.currentPage = productState.currentPage;
      this.pagination.totalPage = productState.totalPage;
    },

    async openForm(id) {
      this.successMessage = null;
      this.errorMessage = null;

      await this.$store.dispatch("category/getCategoriesMin", { all: true });
      await this.$store.dispatch("supplier/getSuppliersMin", { all: true });

      if (id) {
        await this.$store.dispatch("product/getProduct", id);
        const productData = this.$store.state.product.product;

        this.product = { ...productData };
        this.picture = productData.imageUrl;
      } else {
        this.product.id = null;
        this.product.name = null;
        this.product.quantity = null;
        this.product.price = null;
        this.product.imageUrl = null;
        this.product.categoryID = null;
        this.product.supplierID = null;

        this.picture = null;
      }
      this.isOpenForm = true;
    },
    closeForm() {
      this.isOpenForm = false;
    },
    async save() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      const params = {
        userID: userInfo.id,
      };

      if (this.product.id) {
        this.product.imageUrl = this.picture;

        const result = await updateProduct(this.product);

        if (result) {
          this.successMessage = "Update product success";
          await this.$store.dispatch("product/getProducts", params);
        } else {
          this.errorMessage = "Update product fail";
        }
      } else {
        this.product.imageUrl = this.picture;

        const result = await createProduct(this.product);
        if (result.isSuccess) {
          this.successMessage = "Create product success";

          await this.$store.dispatch("product/getProducts", params);
        } else {
          this.errorMessage = "Create product fail";
        }
      }
    },
    fileChange(event) {
      this.uploadValue = 0;
      this.picture = null;
      this.imageData = event.target.files[0];
      console.log(this.imageData);
    },
    onUpload() {
      this.picture = null;

      const storageRef = ref(storage, `${this.imageData.name}`);
      const uploadTask = uploadBytesResumable(storageRef, this.imageData);

      uploadTask.on(
        "state_changed",
        (snapshot) => {
          this.uploadValue =
            (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
        },
        (error) => {
          console.log(error);
        },
        () => {
          this.uploadValue = 100;

          getDownloadURL(uploadTask.snapshot.ref).then((url) => {
            this.picture = url;
          });
        }
      );
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