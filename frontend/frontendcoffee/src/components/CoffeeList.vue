<template>
    <v-container>
        <v-row>
            <v-col cols="6" v-for="(coffee, index) in coffees" :key="coffee.name">
                <v-card class="mx-auto" max-width="400">
                    <v-img :src="getCoffeeImage(index)" height="270px" aspect-ratio="16/9" cover></v-img>
                    <v-card-title>{{ coffee.name }}</v-card-title>
                    <v-card-subtitle>Precio: ₡{{ coffee.price }}</v-card-subtitle>
                    <v-card-text>
                        <p>Cantidad disponible: {{ coffee.stock }}</p>
                    </v-card-text>
                    <v-divider></v-divider>
                    <v-card-actions>
                        <v-btn icon @click="decrementQuantity(coffee)">
                            <v-icon>mdi-minus</v-icon>
                        </v-btn>
                        <span>{{ coffee.quantity || 0 }}</span>
                        <v-btn icon @click="incrementQuantity(coffee)">
                            <v-icon>mdi-plus</v-icon>
                        </v-btn>
                        <v-spacer></v-spacer>
                        <v-btn @click="addToCart(coffee)">Agregar al pedido</v-btn>
                    </v-card-actions>
                </v-card>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import axios from 'axios';

export default {
    name: 'CoffeeList',
    data() {
        return {
            coffees: [],
        };
    },
    onMounted() {
        this.fetchCoffees();
    },
    created() {
        this.fetchCoffees();
    },
    methods: {
        fetchCoffees() {
            axios.get('https://localhost:7058/api/Coffee')
                .then(response => {
                    this.coffees = response.data;
                });
        },
        incrementQuantity(coffee) {
            if (coffee.quantity < coffee.stock) {
                coffee.quantity++;
            }
        },
        decrementQuantity(coffee) {
            if (coffee.quantity > 0) {
                coffee.quantity--;
            }
        },
        getCoffeeImage(index) {
            const images = [
                require('@/assets/cafeAmericano.jpg'),
                require('@/assets/capuchino.jpg'),
                require('@/assets/cafeLatte.jpg'),
                require('@/assets/mocachino.jpg'),
            ];
            return images[index];
        },
        addToCart(coffee) {
            if (coffee.quantity > 0) {
                this.$store.commit('ADD_TO_CART', {
                    name: coffee.name,
                    price: coffee.price,
                    stock: coffee.stock,
                    quantity: coffee.quantity,
                });
                coffee.quantity = 0;
            }
        },
    },
};
</script>
