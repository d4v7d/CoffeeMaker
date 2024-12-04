import { createStore } from 'vuex';

export default createStore({
    state: {
        selectedCoffees: [],
    },
    mutations: {
        ADD_TO_CART(state, coffee) {
            const existingCoffee = state.selectedCoffees.find(c => c.name === coffee.name);
            if (existingCoffee) {
                existingCoffee.selectedQuantity += coffee.quantity;
            } else {
                state.selectedCoffees.push({
                    name: coffee.name,
                    price: coffee.price,
                    selectedQuantity: coffee.quantity || 0,
                });
            }
        },
    },
});
