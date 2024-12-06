<template>
    <v-card>
        <v-card-title>Seleccione su pago</v-card-title>
        <v-card-text>
            <v-list>
                <v-list-item v-for="coffee in selectedCoffees" :key="coffee.name">
                    <v-list-item-content>
                        <v-list-item-title>{{ coffee.name }}</v-list-item-title>
                        <v-list-item-subtitle>Cantidad: {{ coffee.quantity }}</v-list-item-subtitle>
                    </v-list-item-content>
                </v-list-item>
            </v-list>
            <p>Total a pagar: ₡{{ total }}</p>
            <v-btn color="primary" @click="openPaymentDialog">Realizar Pago</v-btn>
        </v-card-text>

        <payment-input :total="total" ref="paymentDialog" @payment-confirmed="handlePayment"></payment-input>
    </v-card>
</template>

<script>
export default {
    name: "CoffeeSelection",
    computed: {
        selectedCoffees() {
            return this.$store.state.selectedCoffees;
        },
        total() {
            return this.selectedCoffees.reduce((sum, coffee) => {
                console.log(coffee);
                const quantity = coffee.quantity || 0;
                const price = coffee.price || 0;
                return sum + quantity * price;
            }, 0);
        },
    },
    methods: {
        openPaymentDialog() {
            this.$refs.paymentDialog.openDialog();
        },
        handlePayment(payment) {
            if (payment >= this.total) {
                alert("Pago realizado exitosamente. ¡Gracias!");
            }
        },
    },
};
</script>
