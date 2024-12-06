<template>
    <v-dialog v-model="dialog" max-width="400px">
        <v-card>
            <v-card-title class="text-h5">Su vuelto</v-card-title>
            <v-card-text>
                <p>{{ message }}</p>
                <p>Monto de la compra: ₡{{ totalAmount }}</p>
                <p>Su pago: ₡{{ paymentAmount }}</p>
                <p>Su vuelto: ₡{{ totalChange }}</p>
                <p>Desglose del vuelto:</p>
                <ul>
                    <li v-for="coin in changeBreakdown" :key="coin.value">
                        ₡{{ coin.value }}: {{ coin.quantity }}
                    </li>
                </ul>
            </v-card-text>
            <v-card-actions>
                <v-btn color="primary" @click="closeDialog">Cerrar</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
export default {
    name: 'ChangeDisplay',
    props: {
        totalAmount: {
            type: Number,
            required: true,
        },
        paymentAmount: {
            type: Number,
            required: true,
        },
        totalChange: {
            type: Number,
            required: true,
        },
        changeBreakdown: {
            type: Array,
            required: true,
        },
        message: {
            type: String,
            required: true,
        },
    },
    data() {
        return {
            dialog: false,
        };
    },
    methods: {
        openDialog() {
            this.dialog = true;
        },
        closeDialog() {
            this.dialog = false;
            this.$emit('refresh'); // Emit an event to refresh the main view
        },
    },
};
</script>