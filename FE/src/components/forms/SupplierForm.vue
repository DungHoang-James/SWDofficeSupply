<template>
  <div>
    <div class="pop-up-container"></div>
    <div class="pop-up-form">
      <p>Supplier Infomation</p>
      <div class="form-container">
        <div class="form-group">
          <label for="categoryName">Name Supplier</label>
          <input
            type="text"
            class="form-control"
            id="categoryName"
            placeholder="Enter name of supplier"
            v-model="supplier.name"
          />
        </div>
        <div class="form-group">
          <label for="categoryDescription">Address</label>
          <input
            class="form-control"
            id="categoryDescription"
            v-model="supplier.address"
          />
        </div>
        <div class="form-group">
          <label for="categoryDescription">Phone Number</label>
          <input
            class="form-control"
            id="categoryDescription"
            v-model="supplier.phoneNumber"
          />
        </div>
        <div class="form-group">
          <label for="categoryDescription">Email</label>
          <input
            class="form-control"
            id="categoryDescription"
            v-model="supplier.email"
          />
        </div>
        <div class="form-row" v-if="supplier.id">
          <label class="mr-3">Deleted:</label>
          <base-switch
            v-model="supplier.isDelete"
            :value="supplier.isDelete"
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
import { createSupplier, updateSupplier } from "../../services/SupplierService";

export default {
  name: "category-form",
  async created() {
    if (this.id) {
      await this.$store.dispatch("supplier/getSupplier", this.id);
      this.supplier = { ...this.supplierData };
    }
  },
  props: {
    id: {
      type: Number,
      default: null,
    },
  },
  data() {
    return {
      supplier: {
        id: null,
        name: null,
        address: null,
        phoneNumber: null,
        email: null,
        isDelete: null,
      },
      successMessage: null,
      errorMessage: null,
    };
  },
  computed: {
    supplierData() {
      return this.$store.state.supplier.supplier;
    },
  },
  methods: {
    closeForm() {
      this.$emit("input", false);
    },
    async save() {
      if (this.supplier.id) {
        const result = await updateSupplier(this.supplier);

        if (result) {
          this.successMessage = "Update supplier success";

          await this.$store.dispatch("supplier/getSuppliers");
          this.$emit("input", false);
        } else {
          this.errorMessage = "Update supplier fail";
        }
      } else {
        const result = await createSupplier(this.supplier);

        if (result.isSuccess) {
          this.successMessage = "Create supplier success";

          await this.$store.dispatch("supplier/getSuppliers");
          this.$emit("input", false);
        } else {
          this.errorMessage = "Create supplier fail";
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