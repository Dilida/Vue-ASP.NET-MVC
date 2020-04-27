<template>
    <div>
        <p>
            Hello from Feature 4444444
        </p>
        <ul id="example-4">
            <li>{{serverdata.message}}</li>
            <li>{{serverdata.alert}}</li>
            <li>{{serverdata.guid}}</li>
        </ul>
    </div>
</template>

<script>export default {
        data() {
            return {
                serverdata: window.serverdata
            }
        },
    }</script>

<style>
</style>
