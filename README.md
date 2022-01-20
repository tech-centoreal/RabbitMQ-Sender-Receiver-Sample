# RabbitMQ Sender Receiver Sample

### Producer

> The user application that sends the message(s)

### Queue

> A buffer that stores the messages

### Consumer

> The user application that receives the message(s)

### Exchange

- Producer can only send messages to an exchange.
- An `exhange` receives messages from producers and then pushes them to queues.
- The `exchange` must know exactly what to do with a message it receives. 
- The rules for that are defined by the `exchange type`.
  - Should it be appended to a particular queue? 
  - Should it be appended to many queues? 
  - Should it get discarded.

### Exchange Types

> Exchange types define how the message should be processed/distrubuted by producer.

- direct
  - A message goes to the queues whose binding key exactly matches the routing key of the message

- topic
  - Similar to direct, except
    - `*` (star) can substitute for exactly one word.
    - `#` (hash) can substitute for zero or more words.
  - Messages sent to a topic exchange can't have an arbitrary routing_key 
  - Routing key must be a list of words, delimited by dots.
  - Routing key has limit of 255 bytes.

- headers

- fanout
  - Broadcasts all the messages it receives to all the queues it knows.

### Work (Task) Queue

- Used to distribute time-consuming tasks among multiple workers.

### Binding

> Relationship between exchange and a queue is called a binding.

## Notes

- RabbitMQ uses Round-Robin to send messages to the consumers/workers

## References: