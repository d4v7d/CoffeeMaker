<template>
    <v-dialog v-model="dialog" max-width="400px">
        <v-card>
            <v-card-title class="text-h5">Realizar Pago</v-card-title>
            <v-card-text>
                <v-container>
                    <v-row>
                        <v-col cols="3">
                            <v-btn @click="addToPayment(500)">₡500</v-btn>
                        </v-col>
                        <v-col cols="3">
                            <v-btn @click="addToPayment(100)">₡100</v-btn>
                        </v-col>
                        <v-col cols="3">
                            <v-btn @click="addToPayment(50)">₡50</v-btn>
                        </v-col>
                        <v-col cols="3">
                            <v-btn @click="addToPayment(25)">₡25</v-btn>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col>
                            <v-btn block @click="addToPayment(1000)">₡1000</v-btn>
                        </v-col>
                    </v-row>
                </v-container>
                <p>Mi pago: ₡{{ payment }}</p>
                <p>A pagar: ₡{{ total }}</p>
            </v-card-text>
            <v-card-actions>
                <v-btn color="red" text @click="clearPayment">Limpiar</v-btn>
                <v-spacer></v-spacer>
                <v-btn color="green" text :disabled="!canPay" @click="confirmPayment">Pagar</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
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
        };
    },
    computed: {
        canPay() {
            return this.payment >= this.total;
        },
    },
    methods: {
        addToPayment(amount) {
            this.payment += amount;
        },
        clearPayment() {
            this.payment = 0;
        },
        confirmPayment() {

            this.dialog = false;
        },
        openDialog() {
            this.dialog = true;
        },
    },
};
</script>