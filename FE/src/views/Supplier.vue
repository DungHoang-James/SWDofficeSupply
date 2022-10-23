<template>
      <div>
    <base-header type="gradient-success" class="pb-6 pb-8 pt-5 pt-md-8">
    </base-header>

    <!-- table -->
    <div class="container-fluid mt--7">
      <div class="row">
        <div class="col">
          <SupplierTable title="Supplier"/>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import SupplierTable from "./Tables/SupplierTable.vue";

export default {
  components: {
    SupplierTable,
  },
};
</script>