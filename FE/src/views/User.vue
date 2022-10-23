<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-8 pt-5 pt-md-8">
    </base-header>

    <!-- table -->
    <div class="container-fluid mt--7">
      <div class="row">
        <div class="col">
          <UserTable title="Users" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import UserTable from "./Tables/UserTable.vue";

export default {
  components: {
    UserTable,
  },
};
</script>