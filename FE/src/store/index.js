import { createStore } from "vuex";
import CategoryModule from './modules/CategoryModule';
import AreaModule from "./modules/AreaModule";
import CompanyModule from "./modules/CompanyModule";
import SupplierModule from "./modules/SupplierModule";
import UserModule from "./modules/UserModule";
import OrderModule from "./modules/OrderModule";
import DepartmentModule from "./modules/DepartmentModule";
import OrderDetailModule from "./modules/OrderDetailModule";
import PeriodModule from "./modules/PeriodModule";
import MenuModule from "./modules/MenuModule";
import ProductModule from "./modules/ProductModule";
import StatisticsModule from "./modules/StatisticsModule";

export default createStore({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    category: CategoryModule,
    area: AreaModule,
    company: CompanyModule,
    supplier: SupplierModule,
    user: UserModule,
    order: OrderModule,
    department: DepartmentModule,
    orderDetail: OrderDetailModule,
    period: PeriodModule,
    menu: MenuModule,
    product: ProductModule,
    statistics: StatisticsModule,
  },
});
