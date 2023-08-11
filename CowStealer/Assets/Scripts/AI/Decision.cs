namespace AI {
    public abstract class Decision {
        public virtual void act(FarmerBehaviour farmer) {}
        
        public virtual void act(AnimalBehaviour animal) {}

        public virtual void act(BullyBehaviour bully) {}
    }
}