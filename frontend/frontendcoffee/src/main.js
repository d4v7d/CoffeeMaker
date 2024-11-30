import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHistory } from 'vue-router';

import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';

import HomePage from './view/HomePage.vue';
import CoffeeList from './components/CoffeeList.vue';
import CoffeeSelection from './components/CoffeeSelection.vue';
import ChangeDisplay from './components/ChangeDisplay.vue';
import PaymentInput from './components/PaymentInput.vue';

const vuetify = createVuetify({
    components,
    directives,
    theme: {
        dark: true,
    },
});

const app = createApp(App);

const router = createRouter({
    history: createWebHistory(),
    routes: [{ path: '/', name: "Home", component: HomePage }],
});

app.use(router)
app.use(vuetify)

app.component('coffee-list', CoffeeList);
app.component('coffee-selection', CoffeeSelection);
app.component('change-display', ChangeDisplay);
app.component('payment-input', PaymentInput);

app.mount('#app')