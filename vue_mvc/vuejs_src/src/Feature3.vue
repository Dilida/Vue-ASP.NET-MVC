<template>
    <div>
        <p>
            Hello from Feature 3
        </p>
        <button @click="callApi()">click me</button>
        <div>{{data}}</div>
        <div>{{firstView}}</div>

    </div>
</template>

<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                data: null,
                firstView:null,
            }
        },
        mounted() {
            this.callMounted()
        },
        methods: {
            callMounted() {
                axios({
                    method: 'get',
                    baseURL: 'http://127.0.0.1:8080',
                    url: '/Helloworld/Welcome',
                    'Content-Type': 'application/json',
                })
                    .then((result) => { this.firstView = result.data })
                    .catch((err) => { console.error(err) })
            },
            callApi() {
                axios({
                    method: 'get',
                    baseURL: 'http://127.0.0.1:8080',
                    url: '/Helloworld',
                    'Content-Type': 'application/json',
                })
                    .then((result) => {this.data = result.data})
                    .catch((err) => { console.error(err) })
            },
        },
    }
</script>

<style>
</style>
