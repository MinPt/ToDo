<template>
    <div class="fallback">
        <Spinner class="fallback__spinner"/>
        <strong class="fallback__text">{{ ` ${text}` }}</strong>
    </div>
</template>

<script>
import Spinner from "@/components/common/Spinner";

export default {
    name: "Fallback",
    components: {Spinner},
    props: {
        text: {
            type: String,
            required: false,
            default: "Loading"
        }
    }
}
</script>

<style scoped lang="scss">
.fallback {
    margin-top: 10rem;
    text-align: center;

    &__spinner {
        font-size: 2rem;
    }

    &__text {
        font-size: 2rem;
    }
}
</style>