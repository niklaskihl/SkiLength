<template>
    <div class="post">
        <label class="header">Ski Length Calculator</label>
        <form @submit.prevent="submit" class="form">
            <div class="input">
                <label class="label">Age</label>
                <div class="control">
                    <input type="number" id="age" v-model="age">
                </div>
            </div>

            <div class="input">
                <label class="label">Length</label>
                <div class="control">
                    <input type="number" id="length" v-model="length">
                </div>
            </div>

            <div class="group">
                <label class="radio">
                    Classic
                    <input type="radio" value="Classic" v-model="skiType" />
                </label>
                <label class="radio">
                    Freestyle
                    <input type="radio" value="Freestyle" v-model="skiType" />
                </label>
            </div>
            <button type="submit" class="btn btn-info">Calculate</button>

            <div class="result">
                <span v-if="fromLength">
                    Your estimated ski length is {{ fromLength }}cm <span v-if="toLength"> to {{ toLength }}cm</span>
                </span>
            </div>
            <div class="error">
                <p v-for="errorMessage in errorMessages" :key="errorMessage.key">{{ errorMessage }}</p>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    interface Data {
        age: number,
        length: number,
        skiType: string
        loading: boolean,
        fromLength: null | number,
        toLength: null | number,
        errorMessages: []
    }

    export default defineComponent({
        data(): Data {
            return {
                loading: false,
                fromLength: null,
                toLength: null,
                errorMessages: [],
                age: 0,
                length: 0,
                skiType: 'Classic'
            };
        },
        methods: {
            submit(): void {
                const requestOptions = {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ age: this.age, length: this.length, skiType: this.skiType })
                };
                fetch('skilength', requestOptions)
                    .then(async response => {
                        const data = await response.json();

                        // check for error response
                        if (!response.ok) {
                            return Promise.reject(data.errors);
                        }

                        this.fromLength = data[0];
                        this.toLength = data[1];
                        this.errorMessages = []
                    })
                    .catch(errors => {
                        this.fromLength, this.toLength = null;
                        this.errorMessages = errors;
                    });
            }
        },
    });
</script>
<style>
    .input {
        padding-top: 20px;
    }
    .header {
        font-size:xx-large;
    }
    form {
        margin: auto;
        width: 320px;
        margin-top: 20px;
        text-align:left;
    }
    .group, .result {
        padding-top: 20px;
        padding-bottom: 20px;
    }
    .radio {
        padding-right:10px;
    }
    .error {
        color: orangered;
    }
</style>