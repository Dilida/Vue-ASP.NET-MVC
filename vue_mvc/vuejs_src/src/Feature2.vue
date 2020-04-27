<template>
    <div>
        <F3 />
        <p>
            Hello from Feature 2
        </p>
        <p>{{source}}</p>
    </div>
</template>

<script>
    import F3 from './Feature3.vue'
    export default {
        data() {
            return {
                source: vueData
            }  
        },
        components: {
            F3
        },
    }
</script>

<style>
</style>
