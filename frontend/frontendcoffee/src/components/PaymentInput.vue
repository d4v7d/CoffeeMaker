<template>
    <v-dialog v-model="dialog" max-width="400px">
        <v-card>
            <v-card-title class="text-h5">Realizar Pago</v-card-title>
            <v-card-text>
                <v-container>
                    <v-row>
                        <v-col cols="3">
                            <v-btn :disabled="canPay" @click="addToPayment(500)">₡500</v-btn>
                        </v-col>
                        <v-col cols="3">
                            <v-btn :disabled="canPay" @click="addToPayment(100)">₡100</v-btn>
                        </v-col>
                        <v-col cols="3">
                            <v-btn :disabled="canPay" @click="addToPayment(50)">₡50</v-btn>
                        </v-col>
                        <v-col cols="3">
                            <v-btn :disabled="canPay" @click="addToPayment(25)">₡25</v-btn>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col>
                            <v-btn block :disabled="canPay" @click="addToPayment(1000)">₡1000</v-btn>
                        </v-col>
                    </v-row>
                </v-container>
                <p>Mi pago: ₡{{ payment }}</p>
                <p>A pagar: ₡{{ total }}</p>
                <p>Monedas seleccionadas:</p>
                <ul>
                    <li>₡500: {{ coins[500] }}</li>
                    <li>₡100: {{ coins[100] }}</li>
                    <li>₡50: {{ coins[50] }}</li>
                    <li>₡25: {{ coins[25] }}</li>
                    <li>₡1000: {{ coins[1000] }}</li>
                </ul>
            </v-card-text>
            <v-card-actions>
                <v-btn color="red" text @click="clearPayment">Limpiar</v-btn>
                <v-spacer></v-spacer>
                <v-btn color="green" text :disabled="!canPay" @click="confirmPayment">Pagar</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>

    <change-display ref="changeDisplay" :total-amount="total" :payment-amount="payment"
        :total-change="changeData.totalChange" :change-breakdown="changeData.changeBreakdown"
        :message="changeData.message" @refresh="refreshPage"></change-display>
</template>

<script>
import axios from 'axios';

export default {
    props: {
        total: {
            type: Number,
            required: true,
        },
    },
    data() {
        return {
            dialog: false,
            payment: 0,
            coins: {
                500: 0,
                100: 0,
                50: 0,
                25: 0,
                1000: 0,
            },
            selectedCoffees: this.$store.state.selectedCoffees,
            changeData: {
                totalChange: 0,
                changeBreakdown: [],
                message: '',
            },
        };
    },
    computed: {
        canPay() {
            return this.payment >= this.total;
        },
    },
    created() {
        this.clearPayment();
    },
    methods: {
        addToPayment(amount) {
            this.payment += amount;
            this.coins[amount]++;
        },
        clearPayment() {
            this.payment = 0;
            this.coins = {
                500: 0,
                100: 0,
                50: 0,
                25: 0,
                1000: 0,
            };
        },
        confirmPayment() {

            const requestPaymentInput = Object.keys(this.coins)
                .map(value => ({
                    Value: parseInt(value),
                    Quantity: this.coins[value],
                }))
                .filter(coin => coin.Quantity > 0);

            const requestCoffees = this.$store.state.selectedCoffees.map(coffee => ({
                Name: coffee.name,
                Price: coffee.price,
                Stock: coffee.stock,
                Quantity: coffee.quantity, // or coffee.selectedQuantity if that's the correct property
            }));

            console.log("Cafes", requestCoffees);
            console.log("Monedas ", requestPaymentInput);

            axios.post('https://localhost:7058/api/Purchase', {
                selectedCoffees: requestCoffees,
                paymentInput: requestPaymentInput,
            })
                .then(response => {
                    console.log('Compra exitosa:', response.data);
                    this.changeData = {
                        totalChange: response.data.totalChange,
                        changeBreakdown: response.data.changeBreakdown,
                        message: response.data.message,
                    };
                    this.$store.commit('CLEAR_CART');
                    this.dialog = false;
                    this.$nextTick(() => {
                        this.$refs.changeDisplay.openDialog();
                    });
                })
                .catch(error => {
                    console.error('Error durante la compra:', error.response ? error.response.data : error.message);
                });
        },
        openDialog() {
            this.dialog = true;
        },
    },
};
</script>