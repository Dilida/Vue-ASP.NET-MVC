<template>
    <div>
        <p>
            Hello from Feature 1
        </p>
        <ul id="example-1">
            <li v-for="item in items" :key="item.first_name">
                {{ item.age }} + {{item.last_name}}
            </li>
        </ul>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                items: [
                    { age: 40, first_name: 'Dickerson', last_name: 'Macdonald' },
                    { age: 21, first_name: 'Larsen', last_name: 'Shaw' },
                    { age: 89, first_name: 'Geneva', last_name: 'Wilson' },
                    { age: 38, first_name: 'Jami', last_name: 'Carney' }]
            }
        },
    }
</script>

<style>
</style>
