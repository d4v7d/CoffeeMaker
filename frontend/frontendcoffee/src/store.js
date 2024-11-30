import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        coffees: [],
        coins: [],
        selectedCoffee: null,
        totalCost: 0,
        paymentInput: '',
        change: 0,
        isOutofService: false,
    },
    /**
     * Mutations y Actions:

        Actualización de cantidades de café y monedas.
        Cálculo del costo total y del vuelto.
        Validaciones de disponibilidad y fondos.
     */
    mutations: {
        setCoffees(state, coffees) {
            state.coffees = coffees;
        },
        setCoins(state, coins) {
            state.coins = coins;
        },
        setSelectedCoffee(state, coffee) {
            state.selectedCoffee = coffee;
        },
        setTotalCost(state, cost) {
            state.totalCost = cost;
        },
        setPaymentInput(state, input) {
            state.paymentInput = input;
        },
        setChange(state, change) {
            state.change = change;
        },
        setIsOutofService(state, isOutofService) {
            state.isOutofService = isOutofService;
        },
    },
});
