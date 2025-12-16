<script setup lang="ts">
import { ref } from 'vue'
import axios from 'axios'

const API_URL = 'http://localhost:5225'

interface Book {
  isbn: string
  title: string
  AvailableAmount: number
}

interface LoanResponse {
  id: string
  maxDevolutionDate: string
}

const book: Book = {
  isbn: '',
  title: '',
  AvailableAmount: 1
}

const loan = ref({
  isbn: '',
  userId: '',
  typeUser: 1
})

const message = ref('')

const createBook = async () => {
  try {
    await axios.post(`${API_URL}/api/books`, book)
    message.value = 'Libro creado correctamente'
  } catch (e: any) {
    message.value = e.response?.data?.message || 'Error al crear libro'
  }
}

const createLoan = async () => {
  try {
    const res = await axios.post<LoanResponse>(`${API_URL}/api/loan`, loan.value)
    message.value = `Préstamo creado. Fecha devolución: ${new Date(res.data.maxDevolutionDate).toLocaleString()}`
  } catch (e: any) {
    message.value = e.response?.data?.message || 'Error al crear préstamo'
  }
}
</script>

<template>
  <div class="container">
    <h1>📚 Indigo Library</h1>

    <section class="card">
      <h2>Agregar Libro</h2>
      <input v-model="book.isbn" placeholder="ISBN" />
      <input v-model="book.title" placeholder="Título" />
      <input v-model.number="book.AvailableAmount" type="number" placeholder="Cantidad" />
      <button @click="createBook">Crear Libro</button>
    </section>

    <section class="card">
      <h2>Crear Préstamo</h2>
      <input v-model="loan.isbn" placeholder="ISBN Libro" />
      <input v-model="loan.userId" placeholder="ID Usuario" />
      <select v-model.number="loan.typeUser">
        <option :value="1">Afiliado</option>
        <option :value="2">Empleado</option>
        <option :value="3">Invitado</option>
      </select>
      <button @click="createLoan">Crear Préstamo</button>
    </section>

    <p class="message">{{ message }}</p>
  </div>
</template>

<style scoped>
.container {
  max-width: 600px;
  margin: auto;
  font-family: system-ui, sans-serif;
}

h1 {
  text-align: center;
}

.card {
  background: #ffffff;
  padding: 1rem;
  margin-bottom: 1rem;
  border-radius: 8px;
  box-shadow: 0 2px 6px rgba(0,0,0,0.1);
}

input, select {
  display: block;
  width: 100%;
  margin-bottom: 0.5rem;
  padding: 0.4rem;
}

button {
  width: 100%;
  padding: 0.6rem;
  background: #42b883;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background: #369870;
}

.message {
  text-align: center;
  font-weight: bold;
}
</style>