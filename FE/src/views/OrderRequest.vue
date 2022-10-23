<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-8 pt-5 pt-md-8">
    </base-header>

    <!-- table -->
    <div class="container-fluid mt--7">
      <div class="row">
        <div class="col">
          <OrderRequestTable title="Order Request" v-if="isManager" />
          <OrderRequestForLeaderTable v-else/>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import OrderRequestTable from "./Tables/OrderRequestTable.vue";
import OrderRequestForLeaderTable from "./Tables/OrderRequestForLeaderTable.vue"
import { LEADER, MANAGER } from "../data/constants/AppConstant";

export default {
  created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

    if (userInfo.roleID === LEADER) {
      this.isManager = false;
    } else if (userInfo.roleID === MANAGER) {
      this.isManager = true;
    }
  },
  components: {
    OrderRequestTable,
    OrderRequestForLeaderTable
  },
  data() {
    return {
      isManager: false,
    };
  },
};
</script>