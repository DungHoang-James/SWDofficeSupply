<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-8 pt-5 pt-md-8">
    </base-header>

    <!-- table -->
    <div class="container-fluid mt--7">
      <div class="row">
        <div class="col">
          <AreaTable title="Areas" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AreaTable from "./Tables/AreaTable.vue";

export default {
  components: {
    AreaTable,
  },
};
</script>