<template>
  <div>
    <div class="pop-up-container"></div>
    <div class="pop-up-form">
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
            />
          </div>
          <div class="form-group">
            <label for="txtQuantity">Quantity</label>
            <input class="form-control" id="txtQuantity" type="number" />
          </div>
          <div class="form-group">
            <label for="txtPrice">Price</label>
            <input class="form-control" id="txtPrice" type="number" />
          </div>
          <div class="form-group">
            <label>Category</label>
            <select class="form-control">
              <option v-for="cate in categoriesMin" :key="cate.id">
                {{ cate.name }}
              </option>
            </select>
          </div>
          <div class="form-group">
            <label>Supplier</label>
            <select class="form-control">
              <option v-for="sup in suppliersMin" :key="sup.id">
                {{ sup.name }}
              </option>
            </select>
          </div>
          <!-- <div class="form-row">
          <label class="mr-3">Deleted:</label>
          <base-switch
            v-model="category.isDelete"
            :value="category.isDelete"
          ></base-switch>
        </div> -->
          <div class="row button-container">
            <button type="button" class="btn btn-success col">Save</button>
            <button type="button" class="btn btn-outline-danger col">
              Cannel
            </button>
          </div>
        </div>

        <div class="form-container">
          <div class="form-group">
            <label for="inputFile">Upload File</label>
            <input
              class="form-control"
              id="inputFile"
              type="file"
              @change="fileChange"
            />
          </div>
          <div>
            <img :src="picture" class="previewImage" v-if="picture" />
            <img src="../../assets/no_image.png" class="previewImage" v-else />
          </div>
          <div class="progress mt-4">
            <div
              class="progress-bar progress-bar-striped bg-success"
              role="progressbar"
              v-bind:style="{ width: uploadValue }"
              :aria-valuenow="uploadValue"
              aria-valuemin="0"
              aria-valuemax="100"
            >
              {{ uploadValue }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as firebase from "firebase/app";

export default {
  name: "product-form",

  async created() {
    await this.$store.dispatch("category/getCategoriesMin", { all: true });
    await this.$store.dispatch("supplier/getSuppliersMin", { all: true });
  },
  data() {
    return {
      imageData: null,
      picture: null,
      uploadValue: 0,
    };
  },
  computed: {
    categoriesMin() {
      return this.$store.state.category.categoriesMin;
    },

    suppliersMin() {
      return this.$store.state.supplier.suppliersMin;
    },
  },
  methods: {
    closeForm() {
      console.log("out");
      this.$emit("input", false);
    },
    fileChange(event) {
      this.uploadValue = 0;
      this.picture = null;
      this.imageData = event.target.files[0];
      console.log(this.imageData);
    },
    onUpload() {
      this.picture = null;
      const storageRef = firebase
        .storage()
        .ref(`${this.imageData.name}`)
        .put(this.imageData);
      storageRef.on(
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
          storageRef.snapshot.ref.getDownloadURL().then((url) => {
            this.picture = url;
          });
        }
      );
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
  width: 1000px;
  height: 700px;
  background: #ffffff;
  border-radius: 15px;
  top: 5%;
  left: 20%;
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

.previewImage {
  width: 440px;
  height: 325px;
  border-radius: 25px;
  border: 1px solid;
}
</style>