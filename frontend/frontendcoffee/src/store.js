import { createStore } from 'vuex';

export default createStore({
    state: {
        selectedCoffees: [],
    },
    mutations: {
        ADD_TO_CART(state, coffee) {
            const existingCoffee = state.selectedCoffees.find(c => c.name === coffee.name);
            if (existingCoffee) {
                existingCoffee.quantity += coffee.quantity;
            } else {
                state.selectedCoffees.push({
                    name: coffee.name,
                    price: coffee.price,
                    stock: coffee.stock,
                    quantity: coffee.quantity,
                });
            }
        },
    },
});
