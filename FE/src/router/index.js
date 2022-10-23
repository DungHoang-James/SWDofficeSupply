import { createRouter, createWebHistory } from "vue-router";

import DashboardLayout from "@/layout/DashboardLayout";
import AuthLayout from "@/layout/AuthLayout";

import Dashboard from "../views/Dashboard.vue";
import Icons from "../views/Icons.vue";
import Maps from "../views/Maps.vue";
import Profile from "../views/UserProfile.vue";
import Tables from "../views/Tables.vue";
import Category from "../views/Category.vue";
import Area from "../views/Area.vue";
import Company from "../views/Company.vue";
import Supplier from "../views/Supplier.vue";
import Product from "../views/Product.vue";
import OrderRequest from "../views/OrderRequest.vue";
import Department from "../views/Department.vue";
import Period from "../views/Period.vue";
import Employee from "../views/Employee.vue";
import Menu from "../views/Menu.vue";
import User from "../views/User.vue";
import Visitor from "../views/Visitor.vue";
import Staff from "../views/Staff.vue";

import Login from "../views/Login.vue";
import Register from "../views/Register.vue";

const routes = [
  {
    path: "/",
    redirect: "login",
    component: AuthLayout,
    children: [
      {
        path: "/login",
        name: "login",
        components: { default: Login },
      },
      {
        path: "/register",
        name: "register",
        components: { default: Register },
      },
    ],
  },
  {
    path: "/main",
    redirect: "/company",
    component: DashboardLayout,
    children: [
      {
        path: "/dashboard",
        name: "dashboard",
        components: { default: Dashboard },
      },
      {
        path: "/icons",
        name: "icons",
        components: { default: Icons },
      },
      {
        path: "/maps",
        name: "maps",
        components: { default: Maps },
      },
      {
        path: "/profile",
        name: "profile",
        components: { default: Profile },
      },
      {
        path: "/tables",
        name: "tables",
        components: { default: Tables },
      },
      {
        path: "/category",
        name: "category",
        components: { default: Category },
      },
      {
        path: "/area",
        name: "area",
        components: { default: Area },
      },
      {
        path: "/company",
        name: "company",
        components: { default: Company }
      },
      {
        path: "/supplier",
        name: "supplier",
        components: { default: Supplier }
      },
      {
        path: "/product",
        name: "product",
        components: { default: Product }
      },
      {
        path: "/menu",
        name: "menu",
        components: { default: Menu }
      },
      {
        path: "/user",
        name: "user",
        components: { default: User }
      },
      // for leader
      {
        path: "/staff",
        name: "staff",
        components: { default: Staff }
      },
      // for manager
      {
        path: "/department",
        name: "department",
        components: { default: Department }
      },
      {
        path: "/orderrequest",
        name: "order request",
        components: { default: OrderRequest }
      },
      {
        path: "/period",
        name: "period",
        components: { default: Period }
      },
      {
        path: "/employee",
        name: "employee",
        components: { default: Employee }
      }
    ],
  },
  {
    path: "/visitor",
    name: "visitor",
    component: Visitor
  }
];

const router = createRouter({
  history: createWebHistory(),
  linkActiveClass: "active",
  routes,
});

export default router;
