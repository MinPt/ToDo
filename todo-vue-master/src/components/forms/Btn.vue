<template>
    <router-link v-if="isLink" class="btn" :class="styles" :to="to" tag="button" :disabled="disabled">
        {{ text }}
    </router-link>
    <button v-else class="btn" :class="styles" :disabled="disabled">
        {{ text }}
    </button>
</template>

<script>
import {computed} from "vue";
import _ from "lodash";

export default {
    name: "Btn",
    props: {
        text: {
            type: String,
            required: true
        },
        to: {
            type: Object,
            required: false,
            default: {}
        },
        classes: {
            type: Object,
            required: false,
            default: {}
        },
        disabled: {
            type: Boolean,
            required: false,
            default: false
        }
    },
    setup(props) {
        const isLink = computed(() => !_.isEmpty(props.to));
        const styles = computed(() => ({...props.classes, "btn_disabled": props.disabled}))

        return {
            isLink,
            styles
        }
    }
}
</script>

<style scoped lang="scss">
.btn {
    display: inline-block;
    padding: 0.35em 1.2em;
    border: 0.1em solid #222831;
    border-radius: 0.12em;
    box-sizing: border-box;
    text-decoration: none;
    font-family: 'Roboto', sans-serif;
    font-weight: 300;
    color: #222831;
    text-align: center;
    transition: all 0.2s;
    user-select: none;
    font-size: 1.2rem;
    background: none;

    &:hover {
        color: #eeeeee;
        background-color: #222831;

        cursor: pointer;
    }

    &_disabled {
        opacity: 50%;

        &:hover {
            color: #222831;
            background: none;
            cursor: not-allowed;
        }
    }
}
</style>