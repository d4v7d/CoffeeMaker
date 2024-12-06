import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHistory } from 'vue-router';

import store from './store';

import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import { aliases, mdi } from 'vuetify/iconsets/mdi-svg';

import HomePage from './view/HomePage.vue';
import CoffeeList from './components/CoffeeList.vue';
import CoffeeSelection from './components/CoffeeSelection.vue';
import ChangeDisplay from './components/ChangeDisplay.vue';
import PaymentInput from './components/PaymentInput.vue';

const vuetify = createVuetify({
    components,
    directives,
    icons: {
        defaultSet: 'mdi',
        aliases,
        sets: {
            mdi,
        },
    },
});

const app = createApp(App);

const router = createRouter({
    history: createWebHistory(),
    routes: [{ path: '/', name: "Home", component: HomePage }],
});

app.use(store);
app.use(router)
app.use(vuetify)

app.component('coffee-list', CoffeeList);
app.component('coffee-selection', CoffeeSelection);
app.component('change-display', ChangeDisplay);
app.component('payment-input', PaymentInput);

app.mount('#app')