<template>
  <div class="layout">
    <NavBar/>
    <Container>
      <router-view/>
    </Container>
    <Footer/>
  </div>
</template>

<script>
import NavBar from "@/components/common/Navbar";
import Footer from "@/components/common/Footer";
import Container from "@/components/common/Container";

export default {
  components: {Container, Footer, NavBar}
}
</script>

<style lang="scss">
* {
  font-family: 'Roboto', sans-serif;
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  background-color: #eeeeee;
  height: 100vh;
  display: grid;
  font-size: 18px;
}

.layout {
  height: 100vh;
  display: grid;
  grid-template-rows: auto 1fr auto;
}
</style>